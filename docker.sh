docker network create n5
docker run -d --name es01 --net n5 -p 9200:9200 -p 9300:9300 -it docker.elastic.co/elasticsearch/elasticsearch:8.3.2

docker run -d --name es01 --net n5 -p 9200:9200 -p 9300:9300 -e "ELASTIC_USERNAME=elastic" -e "ELASTIC_PASSWORD=3s4lv4t13rr4" -e "xpack.security.enabled=false" -e "xpack.security.transport.ssl.enabled=false" -e "http.cors.enabled=false" -e "discovery.type=single-node" docker.elastic.co/elasticsearch/elasticsearch:8.3.2

docker run -d --name zk01 --net n5 -p 2181:2181 -p 2000:2000 -e "ZOOKEEPER_CLIENT_PORT=2181" -e "ZOOKEEPER_TICK_TIME=2000" confluentinc/cp-zookeeper:7.0.0

docker run -d --name bk01 --net n5 -p 9092:9092 -e "KAFKA_BROKER_ID=1" -e "KAFKA_ZOOKEEPER_CONNECT=zk01:2181" -e "KAFKA_LISTENER_SECURITY_PROTOCOL_MAP=PLAINTEXT:PLAINTEXT,PLAINTEXT_INTERNAL:PLAINTEXT" -e "KAFKA_ADVERTISED_LISTENERS=PLAINTEXT://localhost:9092,PLAINTEXT_INTERNAL://bk01:29092" -e "KAFKA_OFFSETS_TOPIC_REPLICATION_FACTOR=1" -e "KAFKA_TRANSACTION_STATE_LOG_MIN_ISR=1" -e "KAFKA_TRANSACTION_STATE_LOG_REPLICATION_FACTOR=1" confluentinc/cp-kafka:7.0.0

docker run -d --name bk01 --net n5 -p 9092:9092 -e "KAFKA_BROKER_ID=1" -e "KAFKA_ZOOKEEPER_CONNECT=zk01:2181" -e "KAFKA_LISTENER_SECURITY_PROTOCOL_MAP=PLAINTEXT:PLAINTEXT,PLAINTEXT_ANY:PLAINTEXT,PLAINTEXT_DOCKER:PLAINTEXT,PLAINTEXT_INTERNAL:PLAINTEXT" -e "KAFKA_ADVERTISED_LISTENERS=PLAINTEXT://localhost:39092, PLAINTEXT_ANY://192.168.2.11:9092,PLAINTEXT_DOCKER://bk01:49092,PLAINTEXT_INTERNAL://bk01:29092" -e "KAFKA_OFFSETS_TOPIC_REPLICATION_FACTOR=1" -e "KAFKA_TRANSACTION_STATE_LOG_MIN_ISR=1" -e "KAFKA_TRANSACTION_STATE_LOG_REPLICATION_FACTOR=1" confluentinc/cp-kafka:7.0.0

docker run -d --name vw01 --net n5 -p 8800:80  digitsy/kafka-magic

/bin/kafka-console-consumer --bootstrap-server localhost:9092 --topic permission-event --from-beginning

docker build -f "C:\Users\Anthony\git\n5\n5.webApi\Dockerfile" --force-rm -t n5webapi:1.0.0 --target base  --label "com.microsoft.created-by=visual-studio" --label "com.microsoft.visual-studio.project-name=n5.webApi" "C:\Users\Anthony\git\n5" 

docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=3S4v4t11993" --net n5 -p 1433:1433 -d --name sql01 mcr.microsoft.com/mssql/server:2019-latest

docker run -e "ASPNETCORE_ENVIRONMENT=Docker" -d --name ap01 --net n5 -p 8800:80 -p 8801:443 n5.webapi:1.0.0

docker run -e "AllowedHostsCustom=localhost:8800;htpp://192.168.2.11:8800/*;http://localhost:3000;192.168.2.11:8800" -e "ASPNETCORE_ENVIRONMENT=Docker" -d --name api01 --net n5 -p 8800:80 -p 8801:443 n5.webapi:1.0.0

docker run -e "ASPNETCORE_ENVIRONMENT=Docker" -d --name api01 --net n5 -p 8800:80 -p 8801:443 n5.webapi:1.0.0

docker run -rm -it --net n5 nicolaka/netshoot

-e ASPNETCORE_ENVIRONMENT Docker
--environment Docker

user: elastic
password: 3s4lv4t13rr4

wsl -d docker-desktop
sysctl -w vm.max_map_count=300000