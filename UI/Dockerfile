### STAGE 1: Build ###
# FROM node:15.5.0-alpine3.12 AS build

# WORKDIR /usr/src/app

# COPY package.json package-lock.json ./

# RUN npm install

# COPY . .

# RUN npm run build

### STAGE 2: Run ###
FROM nginx:1.19.6-alpine

#COPY nginx.conf /etc/nginx/nginx.conf

COPY /dist/smartHomeUI /usr/share/nginx/html

COPY /nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 80