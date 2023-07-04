export const environment = {
  production: true,
  //replace this with production data
  apiUrl: 'http://localhost:52242/api',
  identityServerUrl: 'http://localhost:56620/api',
  clientUrl: 'http://localhost:4200',
  clientSettings: {
    authority: 'http://localhost:56620',
    client_id: 'angular_spa',
    redirect_uri: 'http://localhost:4200/auth-callback',
    post_logout_redirect_uri: 'http://localhost:4200',
    response_type:"code",
    scope:"openid profile email LSS_api.CRUD",
    silent_redirect_uri: 'http://localhost:4200/silent-refresh.html'
  }
};
