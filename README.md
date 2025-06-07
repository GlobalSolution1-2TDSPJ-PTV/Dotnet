# 🌊 FloodWatch API

Sistema inteligente para **monitoramento de enchentes** em áreas urbanas e periféricas. A API é responsável pela comunicação entre sensores físicos, armazenamento de leituras e geração de alertas automáticos.

---

## 👥 INTEGRANTES DO GRUPO

- RM559064 - Pedro Henrique dos Santos
- RM556182 - Vinícius de Oliveira Coutinho
- RM557992 - Thiago Thomaz Sales Conceição

---

## 🧠 Visão Geral

FloodWatch tem como objetivo reduzir impactos causados por enchentes através de sensores distribuídos que monitoram o nível da água e emitem alertas automáticos. Os dados são enviados para essa API que:

- Registra sensores e leituras
- Gera alertas automaticamente com base nas leituras
- Permite consulta e resolução dos alertas

---

## 📐 Diagrama da Solução

### Diagrama Geral da Arquitetura:

```plaintext
                      +----------------------+
                      |    Dispositivos IoT  |
                      | (Sensores de Nível)  |
                      +----------+-----------+
                                 |
                                 v
         +---------------------------------------------+
         |          FloodWatch .NET Web API            |
         |---------------------------------------------|
         | - SensorController                           |
         | - SensorReadingController                    |
         | - AlertController                            |
         +---------------------------------------------+
                                 |
                                 v
                +-------------------------------+
                |     Banco de Dados SQL        |
                | (Tabelas: Sensor, Reading...) |
                +-------------------------------+
```

## Controllers e Funcionalidades

### SensorController
- **GET /api/sensor**: Lista todos os sensores.
- **GET /api/sensor/{id}**: Retorna sensor por ID.
- **POST /api/sensor**: Cria um novo sensor.
- **PUT /api/sensor/{id}**: Atualiza um sensor existente.
- **DELETE /api/sensor/{id}**: Exclui um sensor.

### SensorReadingController
- **GET /api/sensorreading**: Lista todas as leituras.
- **GET /api/sensorreading/{id}**: Retorna leitura por ID.
- **POST /api/sensorreading**: Cria nova leitura e, se nível crítico, cria alerta automaticamente.
- **PUT /api/sensorreading/{id}**: Atualiza leitura existente.
- **DELETE /api/sensorreading/{id}**: Exclui uma leitura.

### AlertController
- **GET /api/alert**: Lista todos os alertas.
- **GET /api/alert/{id}**: Retorna alerta por ID.
- **PUT /api/alert/{id}**: Atualiza alerta (exemplo: marcar como resolvido).
- **DELETE /api/alert/{id}**: Exclui alerta.

---

# Testes

### Sensores

{
  "type": "WATER_LEVEL",
  "localizacao": "Rua das Flores, 123 - Bairro Jardim",
  "latitude": -23.55052,
  "longitude": -46.633308,
  "isActive": true
}

{
  "type": "RAIN",
  "localizacao": "Rua das Flores, 123 - Bairro Jardim",
  "latitude": -23.55052,
  "longitude": -46.633308,
  "isActive": true
}

### Leituras

{
  "sensorId": "adicioane aqui o id gerado pelo post do sensor",
  "sensorValue": 1.8
}

quando rodar esse gerará um alerta automaticamente pois o limite 1.5

{
  "sensorId": "adicioane aqui o id gerado pelo post do sensor",
  "sensorValue": 1.0
}

---

## ⚙️ Configuração e Execução

### 🔧 1. Configurar a Connection String

No arquivo `appsettings.json`, configure a string de conexão com o banco Oracle:

```json
"ConnectionStrings": {
  "Oracle": "Data Source=oracle.fiap.com.br:1521/orcl;User ID=SEU_USUARIO;Password=SUA_SENHA;"
}
```
### 2. Realizar a migração

Abri o Package Manager Console e dar o comando

```bash
  Update-Database
```
### 3. Rodar o Projeto

rode o projeto e abra no seu navegador. Normalmente ele fica no http://localhost:5146/swagger/index.html, porem sua ide pode rodar em outra porta então você altera a porta em vez de ser 5146 vai ser outra porta

---

## 🛠 TECNOLOGIAS E ESTRUTURA

- .NET 8
- Entity Framework Core com Oracle
- Swagger/OpenAPI para documentação
- Clean Architecture
- DTOs para comunicação entre camadas
- MappingConfig para mapeamento de entidades
- Migrations para versionamento do banco de dados

---

