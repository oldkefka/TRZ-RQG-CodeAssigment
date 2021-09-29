## TRZ-RQG-CodeAssigment
 By Ricardo Quevedo Grimaldo

## Task 
>	Create a command line application 
>	Get data for last 5 hours 
>	Calculate by the code the following SQL statement 
>	ALL_HOURS table represent all files
>	SQL statement use just to provide you requirements do not use database in your solution

## PreRequisites
- Visual Studio 2019+ or .NET Core SDK runtime
- Internet connection
- At least 6GB Ram

## Configuration
Relevant information in ..\TRZ-RQG-CodeAssigment\TRZ_WikimediaCount\appsettings.json file

|  Key  | Definition | Default  |
|-------|-------------|:------------|
|   BaseURL   | base website where we obtain the files  | https://dumps.wikimedia.org/other/pageviews/   |
|   HoursRequest   | Hours to be processed     | 5      |
|   SizeResult   | Number of rows to show  | 100 |
|   Datetime   | date and time on request ("YYYYMMDD-HH:mm") |      |
|   Temps:Use   | Create a temp file (Y/N)  | N      |
|   Temps:Clean   | Delete temp file in process (Y/N)          | Y |
|   Temps:FileName   | Temp file name     | page-view-exe     |


### Steps:
1 - Get the URL list, with the [Datetime] in UTC, current by default, in a range of time bu [HoursRequest], using [BaseURL] as base path.
2 - Iterate the urls to download the hour information (Page Views file) in memory stream.
3 - Descompress the GZIp the stream in memory and read to get string list.
4 - Instance objets of the class 'HourDetail' and add they to a list.
5 - Everytime, concatenate (group) the list by DomainCode and PageTitle.
6 - Get the top n or [SizeResult] or the ordered descending list.
7 - Show the result.
