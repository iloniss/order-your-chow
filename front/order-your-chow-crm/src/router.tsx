import { Suspense, lazy } from 'react';
import { Navigate } from 'react-router-dom';
import { PartialRouteObject } from 'react-router';

import SidebarLayout from 'src/layouts/SidebarLayout';

import SuspenseLoader from 'src/components/SuspenseLoader';

const Loader = (Component) => (props) =>
  (
    <Suspense fallback={<SuspenseLoader />}>
      <Component {...props} />
    </Suspense>
  );

// Pages

const Dashboard = Loader(lazy(() => import('src/content/dashboard/Main')));
const Products = Loader(lazy(() => import('src/content/product/Actions')));
const AddProduct = Loader(lazy(() => import('src/content/product/Add')));
const EditProduct = Loader(lazy(() => import('src/content/product/Edit')));
const Categories = Loader(lazy(() => import('src/content/category/Actions')));
const AddCategory = Loader(lazy(() => import('src/content/category/Add')));
const EditCategory = Loader(lazy(() => import('src/content/category/Edit')));
const Measures = Loader(lazy(() => import('src/content/measure/Actions')));
const AddMeasure = Loader(lazy(() => import('src/content/measure/Add')));
const AddIngredients = Loader(
  lazy(() => import('src/content/recipe/AddIngredients'))
);
const EditMeasure = Loader(lazy(() => import('src/content/measure/Edit')));
const AddRecipe = Loader(lazy(() => import('src/content/recipe/Add')));
const AddRecipeDescription = Loader(
  lazy(() => import('src/content/recipe/AddDescription'))
);
const Recipes = Loader(lazy(() => import('src/content/recipe/Actions')));
const EditRecipe = Loader(lazy(() => import('src/content/recipe/Edit')));
// Components

// Status

// const Status404 = Loader(lazy(() => import('src/content/pages/Status/Status404')));
// const Status500 = Loader(lazy(() => import('src/content/pages/Status/Status500')));
// const StatusComingSoon = Loader(lazy(() => import('src/content/pages/Status/ComingSoon')));
// const StatusMaintenance = Loader(lazy(() => import('src/content/pages/Status/Maintenance')));

const routes: PartialRouteObject[] = [
  {
    path: '*',
    element: <SidebarLayout />,
    children: [
      {
        path: '/',
        element: <Dashboard />
      }
      //   {
      //     path: 'overview',
      //     element: (
      //       <Navigate
      //         to="/"
      //         replace
      //       />
      //     )
      //   },
      //   {
      //     path: 'status',
      //     children: [
      //       {
      //         path: '/',
      //         element: (
      //           <Navigate
      //             to="404"
      //             replace
      //           />
      //         )
      //       },
      //       {
      //         path: '404',
      //         element: <Status404 />
      //       },
      //       {
      //         path: '500',
      //         element: <Status500 />
      //       },
      //       {
      //         path: 'maintenance',
      //         element: <StatusMaintenance />
      //       },
      //       {
      //         path: 'coming-soon',
      //         element: <StatusComingSoon />
      //       },
      //     ]
      //   },
      //   {
      //     path: '*',
      //     element: <Status404 />
      //   },
    ]
  },
  {
    path: 'dashboard',
    element: <SidebarLayout />,
    children: [
      {
        path: '/',
        element: <Navigate to="/dashboard/main" replace />
      },
      {
        path: 'main',
        element: <Dashboard />
      }
    ]
  },
  {
    path: 'product',
    element: <SidebarLayout />,
    children: [
      {
        path: '/',
        element: <Navigate to="/product/actions" replace />
      },
      {
        path: 'actions',
        element: <Products />
      },
      {
        path: 'add',
        element: <AddProduct />
      },
      {
        path: 'edit',
        element: <EditProduct />
      },
      {
        path: 'category',
        element: <Categories />
      },
      {
        path: 'measure',
        element: <Measures />
      }
    ]
  },

  {
    path: 'measure',
    element: <SidebarLayout />,
    children: [
      {
        path: '/',
        element: <Navigate to="/measure/actions" replace />
      },
      {
        path: 'add',
        element: <AddMeasure />
      },
      {
        path: 'edit',
        element: <EditMeasure />
      }
    ]
  },

  {
    path: 'recipe',
    element: <SidebarLayout />,
    children: [
      {
        path: '/',
        element: <Navigate to="/recipe/actions" replace />
      },
      {
        path: 'add/ingredients',
        element: <AddIngredients />
      },
      {
        path: 'add/description',
        element: <AddRecipeDescription />
      },
      {
        path: 'add/info',
        element: <AddRecipe />
      },
      {
        path: 'list',
        element: <Recipes />
      },
      {
        path: 'edit',
        element: <EditRecipe />
      }
    ]
  },
  {
    path: 'category',
    element: <SidebarLayout />,
    children: [
      {
        path: '/',
        element: <Navigate to="/category/actions" replace />
      },
      {
        path: 'add',
        element: <AddCategory />
      },
      {
        path: 'edit',
        element: <EditCategory />
      }
    ]
  }
];

export default routes;
