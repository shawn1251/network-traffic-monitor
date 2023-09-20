using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using System.Windows.Input;
using System.Management;

namespace network_monitor.ViewModels;
public class MainViewModel : ObservableObject
{

    public NetworkInterface DataNetworkInterface { get; } = new NetworkInterface();
    public MainViewModel()
    {
        Debug.WriteLine("hererer");
    }
}
public class NetworkInterface
{
    public Dictionary<string,string> InterfaceList { get; } = new Dictionary<string, string>();
    public void RefreshDeviceList()
    { 
        try
        {
            ObjectQuery query = new ObjectQuery("SELECT Name FROM Win32_PerfFormattedData_Tcpip_NetworkInterface");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
            ManagementObjectCollection queryCollection = searcher.Get();

            foreach (ManagementObject m in queryCollection)
            {
                if (!InterfaceList.ContainsKey(m["Name"].ToString()))
                {
                    InterfaceList.Add(m["Name"].ToString(), m["Name"].ToString());
                }
                
            }
        }
        catch (ManagementException e)
        {
            Debug.WriteLine("Error: " + e.Message);
        }
    }
}


