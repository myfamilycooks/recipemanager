FROM postgres:alpine
ADD ./init /docker-entrypoint-initdb.d/
RUN sed -i 's/\r//' /docker-entrypoint-initdb.d/01-mfcdb.sh 