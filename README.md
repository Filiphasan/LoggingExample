# LoggingExample
Dotnet projelerinde Loglama ve Monitoring

## Kullanılan Teknolojiler
- SeriLog
- ElasticSearch
- Kibana
- Docker with Dockercompose
- Elastic APM

## Nasıl Kullanılır?
- Bilgisayarınızda Docker Desktop kurulu ve çalışıyor olması gerekiyor.
- Projeyi Git CLI ile localinize klonlayın
- Proje ana dizininde alttaki komutu çalıştırmanız yeterlidir.
- ```shell
  docker compose up -d --build
  ```
- Proje bağımlılıkları ile birlikte Docker ile ayağa kalkmış olacaktır. 5001 portundan erişebilirsiniz
