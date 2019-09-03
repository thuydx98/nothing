import mongoose from 'mongoose';
mongoose.Promise = global.Promise
const Schema = mongoose.Schema

const teacherSchema = new Schema({
    name: String,
    id: String,
    age: String,
    email: String,
    phone: String,
})
export default mongoose.model('teacher', teacherSchema);