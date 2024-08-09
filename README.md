# React + .NET starter

## Setup

```sh
npm ci --prefix client
docker compose up -d
```

Start server

```sh
dotnet run --project server/Api
```

Start client

```sh
npm run dev --prefix client
```

## How to use

1. Open <http://localhost:5173/>
2. Enter some credentials and click "Register"
3. Go to <http://localhost:1080/> and click link in email
4. Back at <http://localhost:5173/> you are now able to login
