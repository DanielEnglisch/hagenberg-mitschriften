
# Infrastructure As Code With Docker
Martin Ahrer martin.ahrer@software-craftsmen.at
## Introduction
- **Important**  
	Do not copy/paste code from the script boxes (especially from the solutions). The tool for producing the documents (PDF, HTML) emits line breaks and other (hidden) white space characters that will cause strange errors.
- **Important**  
	Files (such as shell script file)s have to be stored with Unix style line ending (LF). Use an editor capable of storing file accordingly when working on a non-Unix based platform.

## Working With Docker for Windows

We will be using Docker for Windows which is using Microsofts Hyper-V virtualization to run Docker (which is a native Linux technology).

So your workstation has to be booted with Hyper-V enabled! After logging on to the system the Docker for Windows daemon (service) has to be started by starting the Docker for Windows application.

### Some tools we will be using

For working with Docker we will be mostly using a command shell. On Windows use cmd, PowerShell, or bash (if available, e..g Git for windows includes a good bash shell). A simple text editor is sufficient for editing scripts or Dockerfiles. However, an editor such as Atom with syntax coloring for Dockerfiles won’t hurt.
- **Important**  
	For keeping network traffic as low as possible we will be using the alpine image unless stated explicitly.

## Unit 1

Goal of this lab exercise is to start a Docker container running a sh shell and to examine what Docker components have been created and how these relate to each other.

### Checking the Docker installation

Before starting to work with Docker we make sure that we have a proper Docker client and daemon running
### Task 1: Check the installed Docker client

```cmd
docker --version
```

### Task 2: Check the Docker system information

```cmd
docker info
```

### Working with containers and images
#### Task 1: Starting a container

In this task we start a container using the `command docker container run …`​.
- **Note**  
	You can always ask the built-in Docker help using `docker --help` or `docker COMMAND --help`.

This is starting a container, executing a simple bash command, and removing the container after it is stopped.

```bash
docker container run --rm --name hello alpine sh -c "echo Hello"
```

#### Task 2: Explore the Docker objects

For the next task we explore the Docker object that eventually have been created. Try to figure out which container objects and image exist now.
1. How can we make the container we just created visible?
2. What is the state of the container we created?
3. How do we get more details about a container or image?
4. How can we remove the container?

Explore the `docker container` and the `docker image` command.
#### Task 3: Try to find the `hello-world image` and `pull` it.

What did you notice during the pull, what exactly happened?

#### Task 4: Create a container that is staying alive (in running state)

In our earlier example we have started a container with a 'one-shot' command that terminated immediately. Now let’s try to start a container that will stay alive and keeps running in the background. As container command use `sh -c "while true; do sleep 10; echo 'Hello'; done"`.

Also work with the `docker container ls` and `docker container stats` commands.

#### Task 5: Let’s cleanup the objects we have created
1. Stop the container in case it is still running.
2. Remove the container
3. Remove the image

## Unit 2

During this lab exercise we explore how we can attach to a container interactively and modify the container by adding files or even install packages.

`alpine` is a fairly slim image with most common tools stripped off to reduce image size.

### Task 1: Install a alpine package

`alpine` is missing the popular tool curl which is required for some features (like healthcheck conditions). Install the package `curl` using the following sequence of shell scripts.

1. First start the container from our previous example as a background process (as we did before).

2. Then get the container id for the started container.

3. Figure out how to execute a command in a running container `docker container --help`.

4. Run the commands below in an interactive bash shell within the container (you will need the options -ti).

5. Next exit from the container

6. Run the following command within the container `sh -c "apk info curl`"

Install curl
```bash
apk update (1)
apk add curl (2)
```
1. Update the package manager.
2. Install the curl package.

So basically we have a container we have customised to our specifications. If we delete the container, all our modifications are gone as the container’s filesystem will be deleted.

### Task 2: Create a new image from the container

Use the `docker container --help` command to figure out what the appropriate command will be.

### Task 3: Let’s give the image a new name

We have created an image from a container and now let’s assign the name `alpine-with-curl:1.0`. Explore the docker image command to figure out how to name an existing image.

### Task 4: Let’s leave our system in a clean state

For most docker objects we have a management command `prune`. It helps us to remove unused/unreferenced Docker objects. Use the proper prune commands to cleanup the system.

## Unit 3

In the previous unit we have manually setup a simple image with the `curl` tool installed. Let’s move that forward such that we can do an automated build of that image.

1. Create a `Dockerfile` that creates an image based on alpine and installs the curl tool.

2. Build an image with the image (repository) name `alpine-with-curl`.

3. Run a Dockerfile lint tool to check if Dockerfile best practices are met. Use https://hub.docker.com/r/lukasmartinelli/hadolint/

4. Run a command in an instance (container) of the image we just built to check that the `curl` tool really is available to the container.

## Unit 4

### Task 1: Starting a web application

In this assignment we build a Docker image that starts up a Spring Boot based web application. The web application is a Spring Boot Fat JAR with an embedded Tomcat Servlet Container running on port 8080. The JAR file must be started from the directory `/opt/app`.
- **Note**  
	The JAR file is downloadable from https://dl.bintray.com/software-craftsmen/continuousdelivery/at/software-craftsmen/continuousdelivery/latest/continuousdelivery-latest-exec.jar.

### Task 2: Healthcheck

Add a healthcheck based on the command `curl -f http://localhost:8080/api`. Run a container based on this image and watch the health status of the container.
- **Note**  
	You will find the documentation to health checking at https://docs.docker.com/engine/reference/builder/#healthcheck.

#### Task 3: Port binding

Make the web application accessible on the Docker host’s port 80 so we can open the app’s web UI through http://localhost/api.

#### Task 4: lint

Run a Dockerfile lint tool to check if Dockerfile best practices are met. Use https://hub.docker.com/r/lukasmartinelli/hadolint/

## Unit 5

During the previous unit we have implemented a Docker image for a web application. In this unit we reimplement that application using `docker-compose`.

First we create a project directory with a sub directory app where the web application’s Docker related source will reside. We put the `Dockerfile` and all other required files from the previous unit into that directory.

Next we create a `docker-compose.yml` service configuration file in the project directory.

We add a definition for a service named `app` to this service configuration. This service should build an image from the context in the subdirectory `./app`. The service must publish the internal container port to the host’s port 80.
- **Note**  
	We can validate the docker-compose configuration using the command `docker-compose config`.

Now we will be able to run the web application using the command `docker-compose up -d` and access the app’s UI at http://localhost/api.

With `docker-compose logs -f` we can follow the service’s console output.

## Unit 6

The web application we have implemented in the previous unit runs an internal embedded database which we will replace by a full blown PostgreSQL instance.

### Task 1: Add a database service

First we add a subdirectory postgresdb in the project directory.

We add a definition for a service named postgresdb to this service configuration. This service should build an image from the context in the subdirectory ./postgresdb.

The Dockerfile for the database adds a shell script for creating the database. This shell script is run automatically when the database container starts up.

`Dockerfile`:
```bash
FROM postgres:9.6-alpine
LABEL maintainer='Martin Ahrer <this@martinahrer.at>'

ADD create-database.sh /docker-entrypoint-initdb.d/create-database.sh
RUN chmod +x /docker-entrypoint-initdb.d/create-database.sh
```

`create-database.sh`:

```bash
#!/usr/bin/env sh
set -e

function execSql {
    psql -v ON_ERROR_STOP=1 --username "$POSTGRES_USER" --dbname "$POSTGRES_DB" --command="$1"
}

execSql "CREATE ROLE $SPRING_DATASOURCE_USERNAME WITH LOGIN PASSWORD '$SPRING_DATASOURCE_PASSWORD' VALID UNTIL 'infinity';"
execSql "CREATE DATABASE app WITH ENCODING='UTF8' OWNER=$SPRING_DATASOURCE_USERNAME CONNECTION LIMIT=-1;"
```

These files `create-database.sh` and `Dockerfile` go into the postgresdb directory.

The above SQL statements require the following environment variables and values for creating a database user:

- SPRING_DATASOURCE_USERNAME=spring
- SPRING_DATASOURCE_PASSWORD=spring

We put those `environment variables` into a environment block within the service definition of the `postgresdb` service.

### Task 2: Reconfigure web application

Further we must reconfigure the web application to connect to the PostgreSQL instance rather than creating and connecting to an embedded database. So we put the following environment variables into the service definition of the `app` service.

- SPRING_DATASOURCE_URL=jdbc:postgresql://postgresdb/app
- SPRING_DATASOURCE_USERNAME=spring
- SPRING_DATASOURCE_PASSWORD=spring
- SPRING_JPA_HIBERNATE_DDL_AUTO=update

Before we restart the updated services we add a `depends_on` element to the configuration for expressing that the `app` service should be started after the `postgresdb` service.

With the command `docker-compose up -d` we can update and restart the services and `docker-compose logs -f` we can use to follow the services' console output.

In order to shut down and delete the containers we issue the command docker-compose down.

### Task 3: Add data volumes for the database

As we haven’t declared any volumes for making container data persistent, any data stored in the database will be lost.

So we add the following named volumes

- `postgresdb` mapped to the service container directory `/var/lib/postgresql` and
- `postgresdb_data` mapped to `/var/lib/postgresql/data`

### Task 4: Refactor service configuration

As we have repeated and hardcoded some configuration values into the service definition we move that values into a separate configuration file for configuration values. We add a .env file to the project directory and put all environment variables with their values into this file. Also we remove all value assignments with hardcoded configuration values from the service definition files.
- **Note**  
	`docker-compose` will read environment variables from this `.env` or the local environment with the latter with higher precedence.
## Unit 7

During this section we will be working with the `asciidoctor/docker-asciidoctor` image. Asciidoctor is a popular Ruby based toolset for producing documentation based on ASCII documents.

### Task 1: Make yourself familiar with asciidoctor.

Run a container based on the `asciidoctor/docker-asciidoctor` image and connect to it interactively using `bash` shell. Use the simple command below to create a simple AsciiDoc document that we will be using to play with various tools.

```bash
echo '= README' > README.adoc
echo 'This is a simple AsciiDoc document' >> README.adoc
```

Now produce a PDF using the command 'asciidoctor-pdf' and produce a HTML5 document using the 'asciidoctor' command.

### Task 2: Host directory mounted as container volume

As soon as we terminated the container we created earlier, all the files that were produced have been deleted as all container layers are removed. Our next assignment is to create an AsciiDoc documnent locally (in the host filesystem) and then start a container with the current directory mounted as volume. When starting the container connect interactively to the container using a 'bash' shell and again produce a PDF and a HTML5 document.

### Task 3: Named volume

Explore the 'docker volume' commands and then create a named volume 'myasciidocuments'. Repeat the same steps as with Task 2, but this time mount the named volume onto the container directory '/documents'.

### Task 4: Host directory mounted as container volume

Use the Docker image `openjdk:8-jdk-alpine` to compile and run the following Java source code in the local filesystem.

```cpp
class HelloWorld {
    public static void main(String[] args) {
        System.out.println ("Hello World");
    }
}
```

## Unit 8

## Task 1: Run local Docker registry

Run a local Docker registry (based on the image `registry:2`) and push the `alpine` image to it.
- **Important**  
	When done with all of the exercises, stop and delete all containers (running and non-running) in order to free system resources.

## Unit 9
Task 1: Create a Docker machine with docker-machine.

Use one of the following drivers:

- **virtualbox** (or **xhyhve**) on macOs
- **hyperv** on Windows 10+

## Unit 10
## Unit 11+12

### Task 1: Implement a continuous delivery pipeline with fully Docker based build pipeline components.

Follow the instructions from   
https://blog.software-craftsmen.at/blog/2017/cd-infrastructure-as-code.html  
Last updated 2018-10-03 06:06:03 UTC