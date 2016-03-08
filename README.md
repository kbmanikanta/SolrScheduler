.# SolrScheduler
SolrScheduler is a WPF application AND Windows Service which you can use to set Solr imports on a recurring basis. This works as of Solr 5.5.0 and works by sending RESTful commands to a specified Solr server instead of using DIH schedulers.

## License
SolrScheduler is licenses under Apache 2.0. Go nuts.

## Prerequisites
1. Microsoft .NET Framework 4.6.1 (https://www.microsoft.com/en-us/download/details.aspx?id=49981)
2. Visual Studio 2013 or 2015

## Why?
Once I started using Solr, I realized that there was really no good solution out there to perform delta (or full) updates without a big hassle. You can use CRON jobs or Windows Task Scheduler jobs; but these are a pain to maintain and log data for. 

## How does it work?
Simply specify your working directory in **app.config** and drop your configuration files in that directory. The configuration files are simply and you can add as many as you want.

```
{
  "name": "My Job Name",
  "server": "myserver.domain.org/solr",
  "core": "my_core_name",
  "importType": "full-import",
  "enabled": true,
  "clean": true,
  "commit": true,
  "optimize" :  true,
  //Start at 4PM EST on Jan 7, 2016
  "recurrenceStart": 635877792000000000,
  //Recur every 2 seconds
  "recurrenceInterval": 2000
}
```

## Why a Windows Service AND WPF Application?
I like to use the graphical version of the application for debugging and testing my cores. This includes seeing how well they handle the stress and load of repeated imports. I use the service for production, where I don't want a GUI (or an account logged in to make use of it). The WPF application also lets you start and stop the updates of individual cores, instead of starting or stopping all updates across the board.
