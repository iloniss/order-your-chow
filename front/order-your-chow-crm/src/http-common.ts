import axios from 'axios';

const baseURL = 'https://localhost:44387/';
export const productsPath = baseURL + 'images/products/';

export default axios.create({
  baseURL: baseURL + 'api',
  headers: {
    'Content-type': 'application/json',
    'Access-Control-Allow-Origin': '*'
  }
});
