
```sh
docker build . --file Dockerfile --tag portfolio-api-image
```

```sh
docker run -p 3333:3333 -e "PORT=3333" --rm --name portfolio-api-container portfolio-api-image
```