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