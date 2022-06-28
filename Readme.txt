Redux is an open source repository of problems, their solutions, verifiers, and reductions. The project has a particular emphasis on NP-Complete and NP-Hard problems



## Deployment

This application can be deployed via a docker docker image. To do so run the following:

````

docker build -t reduxapi .
docker run -it --rm -p 27000:80 --name reduxapi reduxapi

````
This will start a local server via docker. Note that this server is using production binaries, so warnings will be distinct from the development environment described in the usage section.