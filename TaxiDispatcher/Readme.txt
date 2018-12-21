This is a refactoring exercise in C#, meaning you can change internal structure without changing the externally visible behaviour - what console writes on output. 
Code is doing really simple thing. It manages taxi orderes and stores them in (in memory) database. 
You are welcome to change both client and app part of the application.
All of this is happening in one-dimensional world with only one coordinate for location.
Client code (Program.cs) orders few taxi rides and writes at the end how much one of the taxi drivers earned that day.
There are several taxi providers and each of them have its own price for ride. 
Price for ride also depends on time of a day and type of the ride (between cities or ride within the same city).
When ride is ordered, the closest taxi vehicle is offered to customer (unless the closest vehicle is too far away). Ride is then accepted and performed. 
Notice that location of taxi vehicle after ride is updated.
If naming and structure doesn't suit you, fell free to change them.
This is a small exercise and it shouldn't take more of an hour or two of your time once you get to know apps functionality. 
But again if you are willing to dive into details and good arhitecture practices, you are more then welcome to do so.
Introducing tests to the existing code is more than welcome.
Once you're finished with coding send us your code via email or URL to your GitHub/Bitbucket repository. Please include your source control history.
Should you have any questions or feedback for the assignment, feel free to contact us.