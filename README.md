Garage
======

Code here will be in support of Rob's garage monitor and opener project.  

ToggleOpener is a Netduino Plus .NET app.  It initializes Netduino to monitor a magnetic reed switch sensor on data pin 2 (D2) and triggers a garage door opener on data pin 3 (D3).  The app creates a simple socket server, capable only of 1 connection at this time.  Any telnet can be used to connect to the Netduino.  Upon connecting, the Netduino will immediately print the open/closed status of the garage.  If the sensor status changes, it will be displayed immediately.  Commands can also be given to trigger the garage door remote.  They are:

* OPEN
* CLOSE
* TOGGLE

Since the Netduino knows if the garage is open or closed, an open or close command is checked against the current sensor status and will only trigger the garage door opener if the status does not match.  For example, if the command CLOSE is given, and the sensor indicates the garage is already closed, no action is taken.  If the command is OPEN and the sensor indicates the garage is closed, the garage door remote is triggered and the garage door is opened.  

The TOGGLE command toggles the garage door, regardless of what state (open or closed) the garage is in.  

Functionality is strictly limited to monitoring the sensor status and triggering the garage door remote.  Socket connections are restricted to one to keep the app simple, the Netduino processing load low, and to restrict unauthorized use.