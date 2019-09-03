import express from 'express'
import teachercontroller from '../controllers/teachercontroller';

export const teacherRouter = express.Router();

teacherRouter.post('/create', teachercontroller.create)