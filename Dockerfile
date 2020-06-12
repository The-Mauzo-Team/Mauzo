FROM maven:3-jdk-8-openj9 AS builder

COPY src/Server/src /usr/src/app/src  
COPY src/Server/pom.xml /usr/src/app

WORKDIR /usr/src/app

RUN mvn -f /usr/src/app/pom.xml clean \
    && mvn -f /usr/src/app/pom.xml compile \
    && mvn -f /usr/src/app/pom.xml package \
    && mkdir /tmp/lib \
    && mvn dependency:copy-dependencies -DoutputDirectory=/tmp/lib

FROM adoptopenjdk/openjdk8-openj9:alpine-jre AS runner

RUN mkdir /app /app/bin /app/lib /app/conf
COPY --from=builder /usr/src/app/target/Server-1.0.jar /app/bin/
COPY --from=builder /tmp/lib /app/lib/

VOLUME ["/app/logs"]

EXPOSE 8080
ENV PORT 8080

WORKDIR /app

CMD /opt/java/openjdk/bin/java -cp /app/bin/Server-1.0.jar:/app/lib/* io.Mauzo.Server.ServerApp --server.port=$PORT