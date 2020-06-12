FROM mcr.microsoft.com/dotnet/core/sdk:3.1 as builder
USER root

# install node
RUN apt-get update
RUN apt-get -y install curl gnupg
RUN curl -sL https://deb.nodesource.com/setup_12.x  | bash -
RUN apt-get -y install nodejs

WORKDIR /build

# Reinstall dependencies, if changed
COPY ./package*.json ./
RUN npm install --no-optional

# Copy sources
ADD . .

# test via Expecto (.NET, runs fast)
RUN npm run test-dotnet

# test via Fable.Mocha (compile to node.js, then test via wrapped JS Mocha)
RUN npm run test-node

# build project
RUN npm run build

FROM nginx:1.18.0-alpine

COPY --from=builder /build/public /usr/share/nginx/html

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]