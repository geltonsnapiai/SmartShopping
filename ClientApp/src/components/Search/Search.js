import { useState, useEffect } from 'react';
import SearchBar from './SearchBar';
import ProductList from './ProductList'
import { authFetch } from '../../auth/AuthFetch';

export const Search = () => {
    const [searchQuery, setSearchQuery] = useState('');
    const [products, setProducts] = useState([]);

    useEffect(() => {
        if (searchQuery.length < 3) {
            setProducts([]);
            return;
        }

        const controller = new AbortController();
        const signal = controller.signal;
        const url = "/api/product?" + new URLSearchParams({search: searchQuery});
        
        console.log(`Quering '${searchQuery}'...`);

        authFetch(url, {signal: signal})
            .then(response => response.json())
            .then(data => {
                console.log(`Query '${searchQuery}' returned: `, data);
                setProducts(data);
            });

        return () => {
            console.log(`Aborting query '${searchQuery}'`)
            controller.abort();
        }
    }, [searchQuery]);

    return(
        <>
            <SearchBar placeholder="Enter product" onChange={e => setSearchQuery(e.target.value)} />    
            <ProductList products={products}/>
        </>
    )
}