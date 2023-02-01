import axios from 'axios';

const baseURL = 'https://localhost:443/';
export const productsPath = baseURL + 'images/products/';
export const recipesPath = baseURL + 'images/recipes/';

export default axios.create({
  baseURL: baseURL + 'api',
  headers: {
    'Content-type': 'application/json',
    'Access-Control-Allow-Origin': '*'
  }
});
