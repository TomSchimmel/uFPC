# uFPC
Full Page Caching (FPC) for Umbraco websites.

Full Page Caching (often abbreviated as FPC) is a caching method that allows you to store 
static content from a web page in your cache.

With FPC you can cache the static content of your site, which means that you no longer have 
to make various calculations during the retrieval of the page. After all, the content is not 
changed and can therefore be used by everyone. 
By storing this content in your cache, a page can be built faster.

The first time the Umbrao application is started, cache pages will be created for each 
specific page. On the first visit you will immediately 
notice the speed gains caused by FPC, because all calculations have been performed and 
saved for the first time. Everytime you make edit to your Umbraco pages the cache will 
be regenerated.

When the page is visited after the data has been cached, the static content of the website 
is displayed on the page almost immediately. After all, the calculations no longer need to be 
made, because the outcome is cached. This applies to every visitor who visits the page.

The page loads super fast with every new visit. This is because the static contact is cached. 
The page is cached and therefore does not need to be recalculated.

## How to install?

Download the package at Our Umbraco:
https://our.umbraco.com/packages/developer-tools/ufpc/

--------------------------------
### @author: fungybytes
### @website: https://fungybytes.com
### @email: contact@fungybytes.com
--------------------------------
