# OPEN-FTTH Desktop-Bridge

Enables the communication between desktop applications such as QGIS to the open-ftth system using sockets.

## Requirements running the application

* [Taskfile](https://taskfile.dev/#/installation)
* dotnet dotnet-runtime 8.0

## Configure environment variables using minikube ip and ports

```sh
. ./scripts/set-environment-minikube.sh
```

## Running

Running the application

``` sh
task start
```

Testing

``` sh
task test
```
