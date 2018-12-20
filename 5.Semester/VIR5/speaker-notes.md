# Components of the Docker Engine:
* Server: docker daemon (build, run, deploy containers)
* Rest API: specifies interfaces clients can use to communicate with the server/daemon
* Command Line Interface (CLI): client

# Usecases
Use docker to push applications into a test environment to execute automatic/manual tests. After everything is fixed the image can be pushed to the production environment

# Image layers Dockerfile
Docker >= 1.10: only RUN, ADD, COPY add images layers

# Docker build
the whole build context is sent to the docker daemon recursively -> only add needed files

# Combine apt-get update && apt-get install
If you dont combine those two commands, docker will not see those modified install instructions (like adding a package) . Thus apt-get update is not executed and the cached version is used. The build can potentially get outdate versions of the packages.

# Self healing containers:
* always: unless the container was stopped explicitly (eg docker container stop/kill), it is restarted even if it is in stopped/exit state
* unless-stopped: containers will not be restarted when the daemon restarts, if they are in stopped/exit state
* on-failed: container is restarted if it exited with a non-zero exit code. also it is restarted if the daemon restarts no matter in what exit state they are

# Expose:
container listens to connections on this port at runtime

# Docker-compose
Tool for defining and running multi-container docker applications. Used to configure the services, which can be created and started with a single command