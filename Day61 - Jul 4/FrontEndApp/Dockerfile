FROM node:alpine

WORKDIR /app

RUN npm install -g nodemon

COPY package.json /app

RUN npm install

COPY . /app

EXPOSE 3232

CMD ["npm", "run", "dev"]