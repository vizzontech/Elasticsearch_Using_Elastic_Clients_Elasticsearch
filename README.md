# Elasticsearch Using Elastic.Clients.Elasticsearch Nuget Package  

The NEST client was officially deprecated starting with version 8.13 and will reach its end-of-life by the end of the year. Developers should begin migrating to Elastic.Clients.Elasticsearch to avoid any disruptions.

Why the Change?
1. Simplified Codebase:
   The new client, Elastic.Clients.Elasticsearch, was completely redesigned to make it easier to maintain and develop.

2. Automatic Code Generation:
   Instead of manually managing Elasticsearch's huge API (over 450 endpoints and nearly 3000 types), the new client uses automatic code generation. This ensures the client is always up-to-date and aligned with the Elasticsearch server.

3. Better Long-Term Support:
  Manually maintaining such a vast API had become unsustainable. The new approach focuses on maintainability and consistency, making it easier to manage updates and new   features.

4. Future-Proof Your Applications:
   Migrating to Elastic.Clients.Elasticsearch ensures compatibility with the latest server features and reduces risks from outdated or deprecated functionality.


In short, upgrading to Elastic.Clients.Elasticsearch will simplify your code, ensure long-term support, and keep your applications ready for the future.


# Dependencies 
This project has dependencies of the ELK stack running in the docker environment. To deploy the ELK stack clone repository https://github.com/vizzontech/ELK_Docker 

Open the code in VS code, click terminal, and run the docker-compose command 

```
docker-compose up

```
Check if services are running 
```
docker ps --filter "name=elasticsearch" --filter "name=kibana" --filter "name=logstash"

```

You can view services in the brower 

Elastic search
http://localhost:9200/

Logstash
http://localhost:9600/

Kibana
http://localhost:5601/

# Elasticsearch using Elastic Clients Elasticsearch

This standard .NET MVC project showcases the New York City Airbnb Open Data. The sample CSV data is ingested into Elasticsearch using Logstash during the initial setup of the dependencies, ensuring that the index and sample data are ready for use in this demo application. The application offers CRUD operation and data filtering. 


