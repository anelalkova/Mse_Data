package mk.finki.ukim.mk.labb.repository;

import mk.finki.ukim.mk.labb.bootstrap.DataHolder;
import mk.finki.ukim.mk.labb.model.Album;
import org.springframework.stereotype.Repository;

import java.util.List;

@Repository
public class AlbumRepository {

    public List<Album> findAll() {
        return DataHolder.albums;
    }
    public Album findById(Long id){
        return DataHolder.albums.stream()
                .filter(c-> c.getId().equals(id))
                .findFirst()
                .orElse(null);
    }

}
