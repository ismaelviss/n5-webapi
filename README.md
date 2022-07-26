# n5-webapi
## la siguiente aplicación consta de las siguientes funcionalidades:
* consutar permisos
* solicitar permisos
* modificar permisos
* consultar tipos de permisos

## tecnologias/patrones  usados
* .net 6
* xunit
* DDD
* Repository
* Unit of Work
* Moq

## Para levantar el proyecto se debe realizar los siguientes pasos
#### clonar el proyecto
`git clone https://github.com/ismaelviss/n5-webapi.git`
#### Instalar dependencias
`dotnet build`
#### Levantar el proyecto
`dotnet run`
#### URL
`http://localhost:5000/`

## Se puede levantar todo el proyecto ejecutando el archivo docker-compose.yml que se encuenta en: https://github.com/ismaelviss/n5-webapi.git
### dentro de dicho archivo se encuentran las imagenes de los dos proyectos realizados y las imagenes de bases de datos y kafka.
#### clonar el proyecto
`git clone https://github.com/ismaelviss/n5-webapi.git`
#### ingresar a la carpeta donde se clono el repositorio y ejecutar el siguiente comando
`docker compose up -d`
#### finalmente se levanta el proyecto en 
`http://localhost:8800/`

##### docker compose
![compose](https://raw.githubusercontent.com/ismaelviss/n5-webapi/master/stuff/docker_compose.png)

##### docker hub
![dh](https://raw.githubusercontent.com/ismaelviss/n5-webapi/master/stuff/docker_hub.png)

##### docker ejecutando
![de](https://raw.githubusercontent.com/ismaelviss/n5-webapi/master/stuff/dockers.png)


## Imagenes del proyecto
### lista de permisos
![consulta](https://raw.githubusercontent.com/ismaelviss/n5-webapi/master/stuff/consultar_permiso.png)

### consultar permiso por id
![consulta_id](https://raw.githubusercontent.com/ismaelviss/n5-webapi/master/stuff/consultar_permiso_id.png)

### consultar tipo de permiso
![consulta_tipo](https://raw.githubusercontent.com/ismaelviss/n5-webapi/master/stuff/consultar_tipo_permiso.png)

### solicitar permiso
![solicitar_permiso](https://raw.githubusercontent.com/ismaelviss/n5-webapi/master/stuff/crear_permiso.png)

### modificar permiso
![solicitar_permiso](https://raw.githubusercontent.com/ismaelviss/n5-webapi/master/stuff/modificar_permiso.png)

### Elasticsearch
![elasticsearch](https://raw.githubusercontent.com/ismaelviss/n5-webapi/master/stuff/elasticsearch.png)

### Kafka
![kafka](https://raw.githubusercontent.com/ismaelviss/n5-webapi/master/stuff/eventos_kafka.png)
