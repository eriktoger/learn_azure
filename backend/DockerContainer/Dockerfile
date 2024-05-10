FROM node:10-alpine

ADD . /service
WORKDIR /service

RUN npm install

COPY --chown=node:node . .

EXPOSE 8080

CMD [ "node", "app.js" ]