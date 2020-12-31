import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from '../views/Home'
import Form from '../views/Form'
import Confirmation from '../views/Confirmation'
import Pairings from '../views/Pairings'

Vue.use(VueRouter)

const routes = [
    {
        path: '/',
        name: 'home',
        component: Home
    },
    {
        path: '/form',
        name: 'form',
        component: Form
    },
    {
        path: '/confirmation',
        name: 'confirmation',
        component: Confirmation
    },
    {
        path: '/pairings',
        name: 'pairings',
        component: Pairings
    }
]

const router = new VueRouter({
    mode: 'history',
    base: process.env.BASE_URL,
    routes
})

export default router