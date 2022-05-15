FROM mcr.microsoft.com/mssql/server:2019-CU15-ubuntu-20.04
RUN mkdir -p /usr/work
WORKDIR /usr/work
COPY . /usr/work/
EXPOSE 1433
CMD /bin/bash ./entrypoint.sh
