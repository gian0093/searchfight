# searchfight

Proposed case:

It’s requested to create a software program that consults search engines based on keywords that the user enters, related with names of programming languages and compares the amount of results obtained to identify the winning language and search engine.

Investigation:

As part of the development of the proposed exercise, I did a research on the alternatives that allow obtaining information from search engines and I found the following:

Use of Web Scraping or Crawler techniques: As a concept, these techniques allow collecting information from the web automatically.

• There are libraries such as “ScrapySharp” which allows obtaining information in HTML format from a website.

• Information about crawerls like “GoogleBot”, “Yahoo Sluro” or “MsnBot”.

• APIS, such as “SELENIUM” that allow, through the use of browsers, to manipulate websites.

However, we are not allowed to use external libraries.

Proposed solution:

My software development proposal is to use the “Web Scraping” technique that, through the HTTP protocol and the use of regular expressions, can obtain portions of information from different web pages in order to obtain the value of the total search result that some engines have.

Technical information of the application:

console type application developed in .NET Framework version 4.5.

Application developed in the Visual Studio IDE.

This application works with the search engines “Yahoo” and “Bing”.

references used:

• System.Net.Http: To use the HTTP protocol and in this way consult the search engines.

• System.Text.RegularExpressions: To obtain a section of the web page previously obtained.
