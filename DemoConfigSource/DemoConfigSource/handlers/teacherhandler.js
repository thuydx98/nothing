import teacher from '../models/teachermodel';

export default {
    async createteacher(name, id, age, email, phone) {
        const newteacher = await teacher.create({name, id, age, email, phone});
        return newteacher;
    },
}