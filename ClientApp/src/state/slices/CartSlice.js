import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { authFetch } from "../../auth/AuthFetch";
import { asyncStatus } from "../AsyncStatus";


export const generateStrategies = createAsyncThunk(
    "cartSlice/generateStrategies",
    async (_, thunkApi) => {
        if (thunkApi.getState().cart.products.length === 0) {
            throw "There are no products";
        }
        
        return await authFetch("api/calculator", {
            method: 'POST',
            body: JSON.stringify(thunkApi.getState().cart.products),
            headers: {
                'Content-Type': 'application/json'
            }, 
            signal: thunkApi.signal,
        })
            .then(res => res.json())
            .catch(console.error);
    },
);


export const cartSlice = createSlice({
    name: "cartSlice",
    initialState: {
        products: [],
        strategies: [],
        status: asyncStatus.IDLE,
        error: null,
        view: 'cart'
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
        changeView: (state, action) => {
            if (action.payload === 'cart' || action.payload === 'strategies') {
                state.view = action.payload;
            }
        }
    },
    extraReducers: (builder) => {
        builder
            .addCase(generateStrategies.fulfilled, (state, action) => {
                state.status = asyncStatus.SUCCESS;
                state.strategies = action.payload;
            })
            .addCase(generateStrategies.pending, state => {
                state.status = asyncStatus.LOADING;
            })
            .addCase(generateStrategies.rejected, (state, action) => {
                state.status = asyncStatus.ERROR;
                state.error = action.error;
            });
    }
});

export const { addItem, deleteItem, deleteAll, updateItem, changeView } = cartSlice.actions;

export const cartSelector = store => store.cart.products;
export const cartStatusSelector = store => store.cart.status;
export const cartErrorSelector = store => store.cart.error;
export const cartViewSelector = store => store.cart.view;
export const cartStrategiesSelector = store => store.cart.strategies;