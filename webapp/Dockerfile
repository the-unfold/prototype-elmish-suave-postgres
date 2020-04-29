FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine as builder
RUN apk add tree

# install node
RUN apk add --update nodejs=12.15.0-r1 npm=12.15.0-r1

# Copy and show files
ADD . /build
WORKDIR /build
RUN tree -a

# install project dependencies
RUN npm install

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