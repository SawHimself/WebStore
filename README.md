# Подготовка к запуску приложения
* Загрузите и установите последнюю версию .NET SDK.

* Установите последнии версии необходимых пакетов NuGet: 
  
| NuGet пакет                        | Проект             |
|------------------------------------|--------------------|
| `Microsoft.AspNetCore.OpenApi`     | StoreWebApi       |
| `Microsoft.EntityFrameworkCore`    | StoreWebApi, Persistence |
| `Microsoft.EntityFrameworkCore.Sqlite` | Persistence    |
| `Microsoft.EntityFrameworkCore.Tools`  | Persistence    |
| `NLog`                             | Services          |

Используйте команду ```dotnet add package``` в соответствующих одноименных каталогах проектов

## Подготовка приложения к запуску

В файле конфигураций `StoreWebAPI\appSetings.json` в поле `Kestrel` укажите порт на котором будет работать `Kestrel` , по умолчанию установлен 5000 порт

``` JSON
"Kestrel": {  
  "Endpoints": {  
    "Http": {  
      "Url": "http://localhost:5000"  
    }  
  }
}
```

В файле конфигурации `StoreWebAPI\appSetings.json` в поле `СonnectionStrings` укажите строку подключения к существующей базе данных или выполните миграцию для создания новой базы данных по умолчанию приложение подключается к базе данных `Persistence\LiteDb.db` 

``` JSON
"AllowedHosts": "*",  
"ConnectionStrings": {  
  "DefaultConnection": "Data Source=../Persistence/LiteDb.db" 
}
```

Применение миграций к базе данных
Перейдите в директорию проекта.
Выполните команду для применения миграций:

``` bash
dotnet ef database update --project Persistence\Persistence.csproj --startup-project StoreWebAPI\StoreWebAPI.csproj --context Persistence.AppDbContext --configuration Debug 20241107041136_Initial
```
# Запуск приложения

1) Перейдите в каталог проекта WebAPI `StoreWebAPI\StoreWebAPI\`
2) Убедитесь, что все необходимые пакеты установлены
    ``` bash
    dotnet restore
    ```
4) Запустите приложение
   ``` bash
   dotnet run
   ```

