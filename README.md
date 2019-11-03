# ProjectNamakli
This solution provides api for manga sites by parsing them and transforming into universal from
## Technologies and frameworks
* **.NET Core 2.2** (simple layered architecture + CQRS);
* **Docker** (with internal nginx load balancing);
* **StyleCop.Analyzers** (for static code analysis);
* **Swagger** (for easy api exploring);
* **MediatR** (for CQRS implementation);
* **xUnit.net, Moq, FluentAssertions** (for unit and integration testing stuff);
## How to run
```bash
cd ProjectNamakli
docker-compose build
docker-compose up -d --scale core-app=4
```
**NOTE**: Scaling up to 4 instances is limitation of nginx free version
