
const routes = [
  {
    path: '/',
    component: () => import('layouts/MainLayout.vue'),
    props: true,
    children: [
      { path: '', component: () => import('pages/Index.vue') },
      { path: 'surface', props: true, component: () => import('pages/Surface.vue') },
    ]
  },
  // Always leave this as last one,
  // but you can also remove it
  {
    path: '*',
    component: () => import('pages/Error404.vue')
  }
]

export default routes
