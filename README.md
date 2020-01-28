# AwesomeService
Click here for complete document [https://github.com/mangzee/AwesomeService/blob/master/AwesomeService-HandsOn.docx]
>AwesomeService is a service built to keep the user engaged with the latest news on his machine and make him an Awesome Human Being 😊
>This is a continuously running windows service built over “TopShelf”. The functionality of the service is to trigger a news feed from the New York Times API onto the default browser every minute. The scheduling mechanism is built using “Quartz.net”. 
>However, the service is nothing but a half-baked potato at the moment and it needs an expertise to complete the service and make it functional 😉

## Goal
The goal for you is to complete the service and make it fully functional. 
Some points for your consideration
1.	RepeateMeJob.cs under the Jobs folder is the piece of code which executes every minute.
Description of methods used in RepeateMeJob
a.	Execute – Any logic within this gets executed every minute.
b.	ExtractNewFeed – This is an important method where the functionality has to be completed.
i.	 Fetch the data from the defaultApiUrl.
ii.	 Convert the json data into NewsFeed object. Automapper is preferrable :)
iii.	Check if this NewsFeed object is already present in the database, based on the Url.
1.	If the Url is present, that means this news was already read, so get the next data from the feed. You can do this by increasing the query string “offset” in the url to 1 (similary to defaultApiUrl).
2.	Continue the steps from i to iii (by increasing the Offset + 1 for every loop) until u get a new NewsFeed Url not present in the database.
iv.	If the NewsFeed object is a new one, Save the data into the table NewsFeed
v.	Call the OpenBrowser method with the Url once the data is saved.
PLEASE NOTE: ALL THE RETREIVAL AND SAVING TO DATABASE SHOULD GO VIA NewsFeedRepository.cs and SHOULD NOT USE DBCONTEXT DIRECTLY.
2.	Additionally, Implement the below method in NewsFeedRepository.cs
a.	Find – find all records from the NewsFeed table having the particular URL

3.	Brownie points – 
a.	Implement Automapper for mapping from the API JSON Model to the Database (NewsFeed) Model . Related Nuget libraries are already added for your convenience. 



## Technical Details
•	Framework - .net core 2.1 
•	Database – Entity Framework core with In-memory. 
•	Topshelf as a service and Quartz.net as the scheduler
•	Automapper for object to object mapping
•	Code Map as below.

### Additional Details
The New York Times API is a public service API providing the latest news feed.
The specification for the API is available at the URL https://developer.nytimes.com/docs/timeswire-product/1/routes/content/%7Bsource%7D/%7Bsection%7D.json/get
The required URL with the API key is already available in the app.config of the project with the name “AwesomeNewsAPI”. However you need to keep in mind the query strings which may be useful for your development
Query Parameters
limit	integer
Limits the number of results up to 500.
offset	integer
Sets the starting point of the result set.

