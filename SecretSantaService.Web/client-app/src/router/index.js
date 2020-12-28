import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from '../views/Home.vue'
import Create from '../views/Create.vue'
import Login from '../views/Login.vue'
import CreateSuccess from '../views/CreateSuccess.vue'
import View from '../views/View.vue'

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'home',
    component: Home
  },
  {
    path: '/create',
    name: 'create',
    component: Create
  }, 
  {
    path: '/create/success',
    name: 'create-success',
    component: CreateSuccess
  },
  {
    path: '/login',
    name: 'login',
    component: Login
  },
  {
    path: '/view',
    name: 'view',
    component: View
  }
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
