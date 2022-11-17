import { useState, useEffect } from 'react';
import SearchBar from './SearchBar';
import { ProductList } from './ProductList';
import { useDispatch } from 'react-redux';
import { loadProducts } from '../../state/slices/SearchProductListSlice';
import { store } from '../../state/Store';

export const Search = () => {
    const [searchQuery, setSearchQuery] = useState('');

    useEffect(() => {
        let promise = store.dispatch(loadProducts(searchQuery));

        return () => {
            promise.abort();
        }
    }, [searchQuery]);

    return(
        <>
            <SearchBar placeholder="Enter product" onChange={e => setSearchQuery(e.target.value)} />    
            <ProductList />
        </>
    )
}