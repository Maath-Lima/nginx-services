# Notes about NGINX and the project

## NGINX
* [Nginx Gateway](https://marcospereirajr.com.br/using-nginx-as-api-gateway-7bebb3614e48)
* [Proxy pass and Location](https://dev.to/danielkun/nginx-everything-about-proxypass-2ona)

**For .net APIs:**
```
app.Urls.Add("http://*:<port>");
```
### Load Balance
In this section of the article I went through some log configuration:

```
log_format upstreamlog       '$remote_addr - $remote_user [$time_local] '
                             '"$request" $status $body_bytes_sent '
                             '"$http_referer" "$http_user_agent"'
                             '"upstream" "$upstream_addr"';

access_log /var/log/nginx/access.log upstreamlog;
```

### Cache
`proxy_cache_path`: Sets the path and configuration of the cache
`proxy_cache`: Activates the above

* The local disk directory for the cache is called /tmp/products.
* levels sets up a two‑level directory hierarchy under /tmp/products. If the levels parameter is not included, NGINX puts all files in the same directory.
* keys_zone sets up a shared memory zone for storing the cache keys and metadata such as usage timers.
* max_size sets the upper limit of the size of the cache (to 10 gigabytes in this example). It is optional; not specifying a value allows the cache to grow to use all available disk space.
* inactive specifies how long an item can remain in the cache without being accessed. In this example, a file that has not been requested for 60 minutes is automatically deleted from the cache by the cache manager process, regardless of whether or not it has expired. The default value is 10 minutes (10m).
* NGINX first writes files that are destined for the cache to a temporary storage area, and the use_temp_path=off directive instructs NGINX to write them to the same directories where they will be cached.
[A Guide to Caching with NGINX](https://blog.nginx.org/blog/nginx-caching-guide)
[A Guide to Caching with NGINX v2](https://www.f5.com/company/blog/nginx/nginx-caching-guide)

**How Does NGINX Determine Whether or Not to Cache Something?**

NGINX caches a response only if the origin server includes either the Expires header with a date and time in the future, or the Cache-Control header with the max-age directive set to a non‑zero value.

**Can Cache-Control Headers Be Ignored?**
Yes, with the proxy_ignore_headers directive.

```
proxy_ignore_headers Expires Cache-Control X-Accel-Expires;
proxy_ignore_headers Set-Cookie;
proxy_cache_valid any 1m;
```

[Some discussion about `proxy_cache_valid` vs inactive](https://stackoverflow.com/questions/64151378/nginx-cache-inactive-vs-proxy-cache-valid)

### Rate Limit
`limit_req_zone`: Defines the parameters for rate limiting
`limit_req`: Enables rate limiting within the context where it appears

* Key — Defines the request characteristic against which the limit is applied. In the example, it is the NGINX variable $binary_remote_addr, which holds a binary representation of a client’s IP address. This means we are limiting each unique IP address to the request rate defined by the third parameter.
* Zone — Defines the shared memory zone used to store the state of each IP address and how often it has accessed a request‑limited URL. Keeping the information in shared memory means it can be shared among the NGINX worker processes.
* Rate — Sets the maximum request rate. In the example, the rate cannot exceed 10 requests per second for Users API and 1 request per second for Products API.

[Rate Limiting with NGINX](https://blog.nginx.org/blog/rate-limiting-nginx)

[Discussion about burst and nodelay](https://serverfault.com/questions/630157/whats-the-meaning-of-defining-burst-with-nodelay-option)

