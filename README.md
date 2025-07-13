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

[A Guide to Caching with NGINX](https://www.f5.com/company/blog/nginx/nginx-caching-guide)

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