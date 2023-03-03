//будет два URl адреса API
//1. Для локальной средя разработки
//2. Для производственной среды, работающей в статическом веб приложении Azure
const API_BASE_URL_DEVELOPMENT = 'https://localhost:7110';
const API_BASE_URL_PRODUCTION = 'https://:appname.azurewebsites.net';

//Обьект, содержащий все маршруты для конечных точек
const ENDPOINTS = {
    GET_ALL_POSTS: 'get-all-posts',
    //Получаем сообщение по идентификатору
    GET_POST_BY_ID: 'get-post-by-id',
    CREATE_POST: 'create-post',
    UPDATE_POST: 'update-post',
    DELETE_POST_BY_ID: 'delete-post-by-id'
};
//Содержит все полные маршруты API для IDE
//URL адрес для получения сообщений
const development = {
    API_URL_GET_ALL_POSTS: `${API_BASE_URL_DEVELOPMENT}/${ENDPOINTS.GET_ALL_POSTS}`,
    //Получаем пост по ID
    API_URL_GET_POST_BY_ID: `${API_BASE_URL_DEVELOPMENT}/${ENDPOINTS.GET_POSTS_BY_ID}`,
    API_URL_CREATE_POST: `${API_BASE_URL_DEVELOPMENT}/${ENDPOINTS.CREATE_POST}`,
    API_URL_UPDATE_POST: `${API_BASE_URL_DEVELOPMENT}/${ENDPOINTS.UPDATE_POST}`,
    API_URL_DELETE_POST_BY_ID: `${API_BASE_URL_DEVELOPMENT}/${ENDPOINTS.DELETE_POST_BY_ID}`,
};

const production = {
    API_URL_GET_ALL_POSTS: `${API_BASE_URL_PRODUCTION}/${ENDPOINTS.GET_ALL_POSTS}`,
    API_URL_GET_POST_BY_ID: `${API_BASE_URL_PRODUCTION}/${ENDPOINTS.GET_POSTS_BY_ID}`,
    API_URL_CREATE_POST: `${API_BASE_URL_PRODUCTION}/${ENDPOINTS.CREATE_POST}`,
    API_URL_UPDATE_POST: `${API_BASE_URL_PRODUCTION}/${ENDPOINTS.UPDATE_POST}`,
    API_URL_DELETE_POST_BY_ID: `${API_BASE_URL_PRODUCTION}/${ENDPOINTS.DELETE_POST_BY_ID}`,
};

const Constants = process.env.NODE_ENV === 'development' ? development : production;

//Экспортируем константы по умолчанию, что бы наш другой код мог использовать эти URL-адреса API
export default Constants;
