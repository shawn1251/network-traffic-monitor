using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using System.Windows.Input;
using System.Management;
using System.Security.Cryptography;

namespace network_monitor.ViewModels;
public class MainViewModel : ObservableObject
{

    public NetworkInterface DataNetworkInterface { get; } = new NetworkInterface();
    public MainViewModel()
    {

    }
}
public class NetworkInterface : ObservableObject
{
    public Dictionary<string, string> InterfaceList { get; set; } = new Dictionary<string, string> ();
    private string selectedInterface = "<select an interface>";
    public string SelectedInterface
    {
        // use setter in MVVM architecture.SetProperty is responsible to notify any subscriber (like UI elements)
        get { return selectedInterface; }
        set { SetProperty(ref selectedInterface, value); }
    }
    private string sendRate = "0 Byte/s";
    public string SendRate {
        get { return sendRate; }
        set { SetProperty(ref sendRate, value); }
    }

    private string receivedRate = "0 Byte/s";
    public string ReceivedRate
    {
        get { return receivedRate; }
        set { SetProperty(ref receivedRate, value); }
    }

    private string totalData = "0 Byte";
    public string TotalData {
        get { return totalData; }
        set { SetProperty(ref totalData, value); }
    }

    private bool isMonitoringTraffic = false;
    private Thread monitoringThread;
    public void RefreshDeviceList()
    { 
        try
        {
            // construct ObjectQuery to find the network interface name
            ObjectQuery query = new ObjectQuery("SELECT Name FROM Win32_PerfFormattedData_Tcpip_NetworkInterface");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
            ManagementObjectCollection queryCollection = searcher.Get();

            InterfaceList.Clear();
            foreach (ManagementObject m in queryCollection)
            {
                InterfaceList.Add(m["Name"].ToString(), m["Name"].ToString());
            }            

        }
        catch (ManagementException e)
        {
            Debug.WriteLine("Error: " + e.Message);
        }
    }
    public void StartMonitoringTraffic()
    {
        if (!isMonitoringTraffic)
        {
            isMonitoringTraffic = true;
            monitoringThread = new Thread(MonitorTraffic);
            monitoringThread.Start();
        }
    }
    public void StopMonitoringTraffic()
    {
        isMonitoringTraffic = false;
        monitoringThread?.Join(); // Wait for the monitoringThread to complete
    }
    private string changeRateUnit(ulong rate)
    {
        if (rate >= 1024 * 1024) // Convert to MB if it's larger than 1 MB
        {
            return $"{rate /(1024.0*1024.0):F2} MB";
        }
        else if (rate >= 1024) // Convert to KB if it's larger than 1 KB
        {
            return $"{rate / 1024.0:F2} KB";
        }
        else
        {
            return $"{rate} Bytes";
        }
    }
    public void MonitorTraffic()
    {   
        // use Win32_PerfFormattedData_Tcpip_NetworkInterface instead of Win32_PerfRawData_Tcpip_NetworkInterface
        // ref: https://ethernetandcoffee.gvolk.com/posts/windows-formatted-versus-raw-perf-counters/
        ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_PerfFormattedData_Tcpip_NetworkInterface  WHERE Name='" + selectedInterface + "'");
        
        ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
        ulong accumulatedDataSize = 0;

        while (isMonitoringTraffic)
        {
            ManagementObjectCollection queryCollection = searcher.Get();
            foreach (ManagementObject m in queryCollection)
            {
                ulong bytesReceived = (ulong)m["BytesReceivedPersec"];
                ulong bytesSent = (ulong)m["BytesSentPersec"];
                
                ReceivedRate = $"{changeRateUnit(bytesReceived)}/s";
                SendRate = $"{changeRateUnit(bytesSent)}/s";
                
                accumulatedDataSize += bytesReceived;
                accumulatedDataSize += bytesSent;

                TotalData = $"{changeRateUnit(accumulatedDataSize)}";
                Debug.WriteLine($"{bytesReceived} {bytesSent}");
            }

            // Sleep for a specified interval before checking again (e.g., every 1 second)
            Thread.Sleep(1000);
        }
    }
}


