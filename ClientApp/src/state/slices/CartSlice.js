import { createSlice } from "@reduxjs/toolkit";
import { asyncStatus } from "../AsyncStatus";

export const cartSlice = createSlice({
    name: "cartSlice",
    initialState: {
        products: [],
        status: asyncStatus.IDLE,
        error: null
    },
    reducers: {
        addItem: (state, action) => {
            state.products.push(action.payload);
        },
        deleteItem: (state, action) => {
            state.products = state.products.filter(item => item.id !== action.payload);
        },
        deleteAll: (state) => {
            state.products = [];
        },
        updateItem: (state, action) => {
            let index = state.products.findIndex(item => item.id === action.payload.id);
            if (index > -1) {
                state.products[index] = action.payload;
            }
        },
    },
});

export const { addItem, deleteItem, deleteAll, updateItem } = cartSlice.actions;

export const cartSelector = store => store.cart.products;
export const cartStatusSelector = store => store.cart.status;
export const cartErrorSelector = store => store.cart.error;