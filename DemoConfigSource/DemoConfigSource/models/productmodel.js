import mongoose from 'mongoose';
mongoose.Promise = global.Promise
const Schema = mongoose.Schema

const productSchema = new Schema({
    Name: String,
    price: String,
    origin: String,
    trademark: String,
   
})
export default mongoose.model('product', productSchema);