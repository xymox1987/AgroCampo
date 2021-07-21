# Version inicial del back 

## Descripción del proyecto

Proyecto creado con .net core , cuenta con las siguientes especificaciones:
- Capas que sirven para separar la lógica de negocio : API,BUSINESS,COMMON,DATAACCESS,DOMAIN
- Inyección de dependencias
- Patrón Repository
- Pensado para integrarse con un sistema de autenticación Single Sign On
- API orientada a Micro servicios
- Incluye archivo Dockerfile para dockerización


## Pasos para la ejecución

1. Ejecute el script ubicado en la carpeta ScriptDB, para la creación de la base de datos  
1. Cambiar la cadena de conexión en el archivo appsettings.json
1. Ubiquese en la carpeta AgroCampo_Back/AgroCampo_Api
1. Ejecute el proyecto con el comando 
   ~~~ 
		dotnet run
   ~~~
1. abra el navegador y escriba https://localhost:44326/swagger/index.html
1. utilice swagger para la prueba del API REST en los diferentes verbos HTTP
