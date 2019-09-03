import product from '../models/productmodel';
export default {
    async createproduct({name, price, origin, trademark}) {
        const newproduct = await product.create({name, price, origin, trademark});
        return newproduct;
    },
    async getproduct(productname){
        const user = await product.findOne({productname});
        return user;
    }
}