import { configureStore } from '@reduxjs/toolkit';
import { cartHeaderIconSlice } from './slices/CartHeaderIconSlice';
import { cartSlice } from './slices/CartSlice';
import { productListSlice } from './slices/ProductListSlice';
import { shopsSlice } from './slices/ShopsSlice';
import { uploadProductsSlice } from './slices/UploadProductsSlice';

export const store = configureStore({
    reducer: {
        uploadProducts: uploadProductsSlice.reducer,
        productList: productListSlice.reducer,
        shops: shopsSlice.reducer,
        cart: cartSlice.reducer,
        cartHeaderIcon: cartHeaderIconSlice.reducer,
    },
});