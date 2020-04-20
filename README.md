# HolidayApi

This is an API wrapper for https://github.com/martinjw/Holiday. See the original project for a list
of supported locations.

RPC API fetchs all public holiday of countries below using https://github.com/martinjw/Holiday project. 
The Holiday project is added as a git sub module and you should run following two commands after cloning this project to get Holiday project files.

```git
git submodule init 
git submodule update
```

## Running via docker-compose
After you built docker image, you can run the docker-compose command below to start the service
```
docker-compose -f docker-compose.yml up
```