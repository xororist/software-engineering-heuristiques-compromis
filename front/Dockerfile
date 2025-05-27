
FROM node:20 AS builder

WORKDIR /app


COPY package*.json ./


RUN npm install


COPY . .


RUN npm run build --configuration=production


FROM nginx:alpine


COPY --from=builder /app/dist/front/browser /usr/share/nginx/html


EXPOSE 88

CMD ["nginx", "-g", "daemon off;"]
