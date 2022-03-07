FROM node AS builder
WORKDIR /repo
COPY src/vetrina-dolci-client .
RUN npm install
RUN npm run build -- --prod

FROM nginx AS prod
WORKDIR /site
COPY --from=builder /repo/dist/vetrina-dolci-client .
COPY docker/vetrina-dolci-site.conf /etc/nginx/conf.d/default.conf