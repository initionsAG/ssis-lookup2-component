## initions SSIS Component "Lookup 2"
"Lookup 2" is a custom component for [SQL Server Integration Services](https://en.wikipedia.org/wiki/SQL_Server_Integration_Services). It can be added to the Data Flow of an SSIS-package to look up data ranges. 
//im gegensatz zur standard-lookup-komponente, die nur auf einen gleichen Wert prüfen kann.
// Bsp ist ein LU auf einen Datumsbereich für eine Währungstabelle (gültig von gültig bis)

### Requirements for development
* Microsoft VisualStudio 2012 or greater

### Requirements for usage
* Microsoft SSIS 2008R2 / 2012 / 2014

### How to use
1. open SSIS Package
2. create DFT and open it 
3. // get src-data 
4. // create new column to hold lookup2-return-value
3. find Lookup2 component in SSIS Toolbox
3. ![SSIS Toolbox](/resources/SSIS_Toolbox_LOG.PNG "SSIS Toolbox with LOG component")
4. add "Lookup2" component to Data Flow and connect its input to output of previous Derived-Column-Task
5. ![Control Flow](/resources/Control_Flow_LOG.PNG "Control Flow with connected LOG component")
6. open and configure "LOG" component:
    * choose "Component Type" (initialize, information, item begin,...)
    * add "Message Text" (like "DFT started")
    * add any "Variable" you like to the message text (i.e. ContainerStartTime)
    * ![LOG_02](/resources/LOG_02.PNG "Edit LOG component")
7. result is written to 
    * SSIS Output
      * ![Output](/resources/Output_LOG.PNG "Output") 
    * Exectuion Results 
      * ![Execution Results](/resources/Execution_Results.PNG "Execution Results") 

### Bugs
If you find a bug, please contact us on GitHub

### Changelog
2016-02-xx
First Release

### License
[MIT License](LICENSE)