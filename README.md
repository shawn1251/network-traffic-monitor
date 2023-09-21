# Network Monitor
## Overview
Network Monitor is a WPF GUI application developed in C# that uses Windows Management Instrumentation (WMI) to retrieve network interface information and real-time network traffic data. It provides a user-friendly interface for monitoring and visualizing network traffic rates in kilobytes per second (KB/s).

## Features

	1. Network Interface Discovery: Network Monitor uses WMI to discover available network interfaces on your system.

	2. Real-Time Traffic Rate: The application updates and displays real-time data for both sent and received network traffic rates in KB/s.

	3. Interactive Visualization: Network traffic rates are visualized using the LiveCharts library, providing an interactive and dynamic charting experience.

## Tools and Technologies

* **C#**: The project is developed using the C# programming language, a versatile language commonly used for Windows application development.

* **Windows Presentation Foundation (WPF)**: The graphical user interface (GUI) of the application is built using WPF, a framework for creating Windows desktop applications with rich user interfaces.

* **Windows Management Instrumentation (WMI)**: WMI is a powerful framework provided by Windows for retrieving system and device execution data. It is used to gather network interface and traffic data in this project.

* **PowerShell**: PowerShell is utilized to interact with WMI and explore system data. It is used for discovering available namespaces, classes, and executing queries to obtain the required information.

* **LiveCharts**: LiveCharts is employed for visualizing real-time network traffic data in the form of interactive charts and graphs. It enhances the user experience by providing dynamic and responsive data visualization.

## Getting Started
To get started with Network Monitor, follow these steps:

Clone the Repository: Clone this GitHub repository to your local machine using the following command:
```shell
git clone https://github.com/your-username/network-monitor.git
```

Build and Run: Open the project in a C# IDE (such as Visual Studio) and build the solution. Run the application to start monitoring your network traffic.

## Usage
	1. Launch the Network Monitor application.
	2. Select a network interface from the available list.
	3. The application will display real-time traffic rates in both sent and received directions.
	4. Network traffic rates are visualized in an interactive chart.


## Working with WMI (Windows Management Instrumentation)

WMI is a powerful tool that allows you to access system and device execution data on your PC. You can use PowerShell to explore and interact with WMI to find the specific parameters and information you are interested in.

### Listing WMI Namespaces

WMI uses namespaces to organize its classes. To list all available namespaces, you can use the following PowerShell command:

```powershell
Get-WmiObject -Namespace Root -Class __Namespace | Select-Object -Property Name
```


### Listing Classes in a Specific Namespace
If you are looking for classes within a particular namespace, you can specify it in your query. Here's an example of how to search for classes containing the word "storage" within the root/wmi namespace:

```powershell
Get-WmiObject -List -Class "*storage*" -Namespace "root/wmi" | Format-Table -AutoSize
```
This command will list all classes with "storage" in their name within the specified namespace.

### Retrieving Data Using WMI
Once you have identified the target class you're interested in, you can use the gwmi alias to execute the query and retrieve data. For example, to get network interface data, you can use the following command:

```powershell
gwmi -Class Win32_PerfFormattedData_Tcpip_NetworkInterface
```

Congratulations! You have successfully obtained the data you wanted. You can now use WQL (WMI Query Language), which is similar to SQL, to refine your queries for your project.

### Using WQL Queries
You can use WQL queries to filter and retrieve specific data from WMI classes. For instance, if you want to retrieve network interface data for a particular interface name, you can use a query like this:


`SELECT * FROM Win32_PerfFormattedData_Tcpip_NetworkInterface WHERE Name="{interface name}"`
Replace {interface name} with the actual name of the network interface you want to query.

Feel free to explore further and use these WMI techniques to gather system and device information for your projects.

## Project Background

This project holds a special place in my journey as it represents my first step into C# GUI development. It was crafted with the aim of gaining hands-on experience and becoming familiar with the world of graphical user interfaces in C#. While it may be a small and humble project, it has been a significant learning experience for me.
I hope this project can serve as an inspiration for fellow beginners who are embarking on their coding journeys.




	