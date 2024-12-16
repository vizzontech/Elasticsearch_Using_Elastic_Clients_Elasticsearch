# Elasticsearch Using Elastic.Clients.Elasticsearch Nuget Package 

The NEST nuget package for Elasticsearch has reached the end of its life and has been replaced with the new nuget package Elastic.Clients.Elasticsearch   

Reason for new package 

- Transition to Elastic.Clients.Elasticsearch: The deprecation of NEST was announced with version 8.13, and it will reach end-of-life by the end of the year. Developers are urged to begin migration efforts to avoid disruptions.

- Simplified and Sustainable Codebase: The new Elastic.Clients.Elasticsearch was redesigned from the ground up to improve maintainability and streamline client development for Elasticsearch.

- Automatic Code Generation: To manage Elasticsearch's extensive API surface, the 8.x clients and many associated types are now automatically generated from a shared specification, ensuring consistency and alignment with the server.

- Reducing Scope for Maintainability: Supporting over 450 endpoints and nearly 3000 types manually became impractical, leading to a focused approach that prioritizes long-term sustainability.

- Future-Proofing Applications: Migrating to Elastic.Clients.Elasticsearch guarantees compatibility with the latest server features while mitigating risks from deprecated functionality.



https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/index.html 
